angular.module('starter.newEmpAbs')
    .controller('newEmpAbsCtrl', ['dataservice', '$scope', '$ionicModal', 'toaster', '$http', '$timeout', function (dataservice, $scope, $ionicModal, toaster, $http, $timeout) {
        var vm = this;
        getList();
        $ionicModal.fromTemplateUrl('employee-absence/newEmployeeAbsence.html', function (modal) {
            var today = new Date();
            vm.startDate = today;
            vm.addDialog = modal;
        }, {
                scope: $scope,
                animation: 'slide-in-up'
            });

        vm.showAddChangeDialog = function (action) {
            if (action == 'add') {
                vm.absence = '';
                vm.endDate = '';
            }
            vm.action = action;
            vm.addDialog.show();

        };

        vm.leaveAddChangeDialog = function () {
            // Remove dialog 
            vm.addDialog.remove();
            var today = new Date();
            vm.startDate = today;
            // Reload modal template to have cleared form
            $ionicModal.fromTemplateUrl('employee-absence/newEmployeeAbsence.html', function (modal) {
                vm.addDialog = modal;
            }, {
                    scope: $scope,
                    animation: 'slide-in-up'
                });
        };

        // get list from API
        function getList() {
            vm.list = dataservice.getEmployeeAbsences().then(function (response) {
                vm.list.data = response;
            });
        }

        // get absenceTYPES
        dataservice.getAbsences().then(function (response) {
            vm.absenceData = response;

        });

        // cache the empty for for Edit Dialog
        vm.saveEmpty = function (form) {
            vm.form = angular.copy(form);
        }

        vm.addEmpAbs = function (form) {
            var userName = getCookie(document.cookie.replace(/(?:(?:^|.*;\s*)userObject\s*\=\s*([^;]*).*$)|^.*$/, "$1"))

            function getCookie(userObject) {
                var parsedCookie = JSON.parse(userObject);
                return parsedCookie.userName;
            };

            vm.loggedIn = dataservice.getLoggedInUser(userName).then(function (response) {
                vm.loggedIn.data = response;

                var newAbsence = {
                    StartDate: form.startdate.$modelValue,
                    EndDate: form.enddate.$modelValue,
                    Employee: {
                        Id: vm.loggedIn.data.Id,
                        Name: vm.loggedIn.data.Name,
                        Email: vm.loggedIn.data.Email,
                        UserName: vm.loggedIn.data.UserName
                    },
                    Absence: {
                        Id: form.absence.$modelValue.Id,
                        Name: form.absence.$modelValue.Name
                    },
                    Removed: false
                }
                if (newAbsence.EndDate !== "" && newAbsence.EndDate < newAbsence.StartDate) {
                    var validationType = 'Tillbaka-datumet är tidigare än Start-datumet';
                }
                if (newAbsence.Absence.Id == undefined) {
                    var validationType = 'Du måste välja en frånvaro';
                }
                if (newAbsence.Absence.Id == undefined && (newAbsence.EndDate !== "" && newAbsence.EndDate < newAbsence.StartDate)) {
                    var validationType = 'Se över din inmatning i: Frånvaro & Tillbaka'
                }
                if (newAbsence.EndDate > newAbsence.StartDate || newAbsence.EndDate == "") {

                    dataservice.postEmployeeAbsence(newAbsence).success(function (response) {
                        vm.value = response;
                        toaster.pop({
                            toasterId: 2,
                            type: 'success',
                            title: 'Tillagd!',
                            body: 'Frånvaron lades till i listan',
                            timeout: 2000
                        })

                        getList();
                        vm.leaveAddChangeDialog();
                        // vm.list.data.push(newAbsence);
                    }).error(function (response) {
                        vm.value = response;

                        toaster.pop({
                            type: 'error',
                            title: 'Fel!',
                            body: validationType,
                            timeout: 2000
                        })
                    })
                } else {
                    toaster.pop({
                        type: 'error',
                        title: 'Fel!',
                        body: 'Tillbaka-datumet är tidigare än Start-datumet',
                        timeout: 2000
                    })
                }
            })
        };

        // REMOVE
        vm.removeItem = function (idx) {
            var empAbsToDelete = vm.list.data[idx];

            dataservice.deleteEmpAbs(empAbsToDelete.Id).then(function (response) {
                vm.list.data.splice(idx, 1);

                toaster.pop({
                    toasterId: 1,
                    type: 'error',
                    title: 'Raderad!',
                    body: 'Frånvaron raderades från listan',
                    timeout: 2000
                });
            });
        };

        vm.showEditAbsence = function (item) {

            vm.tmpEditAbsence = item;

            vm.startDate = new Date(item.StartDate);
            if (item.EndDate != null) {
                vm.endDate = new Date(item.EndDate);
            }
            vm.absence = item.Absence;

            // Open dialog
            vm.showAddChangeDialog('change');

        };

        vm.editAbsence = function (form) {
            var rememberId = vm.tmpEditAbsence;

            var userName = getCookie(document.cookie);

            function getCookie(userObject) {
                var parsedCookie = JSON.parse(userObject);
                return parsedCookie.userName;
            };

            vm.loggedIn = dataservice.getLoggedInUser(userName).then(function (response) {
                vm.loggedIn.data = response;

                var editedAbsence = {
                    Id: rememberId.Id,
                    StartDate: form.startdate.$modelValue,
                    EndDate: form.enddate.$modelValue,
                    Employee: {
                        Id: vm.loggedIn.data.Id,
                        Name: vm.loggedIn.data.Name,
                        Email: vm.loggedIn.data.Email,
                        UserName: vm.loggedIn.data.UserName
                    },
                    Absence: {
                        Id: form.absence.$modelValue.Id,
                        Name: form.absence.$modelValue.Name
                    },
                    Removed: false
                }
                if (editedAbsence.EndDate > editedAbsence.StartDate || editedAbsence.EndDate == "") {
                    dataservice.postEmployeeAbsence(editedAbsence).success(function (data) {
                        vm.value = data;
                        toaster.pop({
                            toasterId: 2,
                            type: 'info',
                            title: 'Uppdaterad!',
                            body: 'Frånvaron Uppdaterades',
                            timeout: 2000
                        })
                        getList();
                        vm.leaveAddChangeDialog();
                    })
                }
                else {
                    toaster.pop({
                        type: 'error',
                        title: 'Fel!',
                        body: 'Tillbaka-datumet är tidigare än Start-datumet',
                        timeout: 3000
                    })
                }
            })
        }
    }
    ]);
