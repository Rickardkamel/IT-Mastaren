(function () {
    'use strict';

    angular.module('starter.lunch')
        .controller('LunchController', LunchController);
    LunchController.$inject = ['$scope', '$ionicModal', 'dataservice', '$ionicSlideBoxDelegate', 'toaster', '$timeout'];
    function LunchController($scope, $ionicModal, dataservice, $ionicSlideBoxDelegate, toaster, $timeout) {
        var vm = this;
        getLunches();

        // Datum-koll
        vm.dateCheck = function (date) {
            date = new Date(date).getDate();
            var today = new Date().getDate();
            if (date == today) {
                return true;
            }
            if (date !== today) {
                return false;
            }
        }

        vm.showButton = function (employeeList) {
            for (var i = 0; i < employeeList.length; i++) {
                if (employeeList[i].Name == vm.loggedIn.data.Name) {
                    return true;
                } 
            }
        }

        // Modal-Instance
        $ionicModal.fromTemplateUrl('newlunch-modal.html', function (modal) {
            var today = new Date();
            vm.lunchDate = today;
            vm.modal = modal;
        }, {
                scope: $scope,
                animation: 'slide-in-up'
            });

        $ionicModal.fromTemplateUrl('newlunch-modal.html', {
            scope: $scope,
            animation: 'slide-in-up',
        }).then(function (modal) {
            vm.modal = modal;
        })

        // Show Modal
        vm.showModal = function () {
            var today = new Date();
            vm.lunchDate = today;
            vm.restaurant = "";
            vm.modal.show();
        }

        // Hide Modal *DO I NEED THIS???*
        vm.hideModal = function () {
            vm.modal.hide();
        }

        // Close Modal
        vm.closeModal = function () {
            vm.modal.remove();
            // Reload Modal
            $ionicModal.fromTemplateUrl('newlunch-modal.html', function (modal) {

                vm.modal = modal;
            }, {
                    scope: $scope,
                    animation: 'slide-in-up'
                });
        }

        //Hämta alla restauranger
        dataservice.getRestaurants().then(function (response) {
            vm.restaurantData = response;
        })

        //Hämta alla luncher
        function getLunches() {
            vm.lunchList = dataservice.getLunches().then(function (response) {
                vm.lunchList.data = response;
            })
        }

        //Hämta user
        var userName = getCookie(document.cookie.replace(/(?:(?:^|.*;\s*)userObject\s*\=\s*([^;]*).*$)|^.*$/, "$1"))
        function getCookie(userObject) {
            var parsedCookie = JSON.parse(userObject);
            return parsedCookie.userName;
        };
        vm.loggedIn = dataservice.getLoggedInUser(userName).then(function (response) {
            vm.loggedIn.data = response;
        })

        // Öppna modal vid + klick
        vm.newLunch = function (lunch) {
            vm.openModal(lunch);
            vm.value = lunch;
        }
        //Hämta lunchen
        // vm.getThisLunch = function (lunch) {
        //     dataservice.getLunch(lunch.Id).then(function (response) {
        //         vm.getThisLunch.data = response;
        //     })
        // }
        //Join lunch
        vm.joinLunch = function (lunch) {

            //Anropa lunchen
            vm.getThisLunch = dataservice.getLunch(lunch.Id).then(function (response) {
                vm.getThisLunch.data = response;


                //Posta Lunchen
                dataservice.postLunch(vm.getThisLunch.data, vm.loggedIn.data).success(function (response) {
                    dataservice.getLunches().then(function (data) {
                        vm.lunchList.data = data;
                    })

                    toaster.pop({
                        toasterId: 2,
                        type: 'success',
                        title: 'Gått med!',
                        body: 'Du har nu gått med i ' + vm.getThisLunch.data.Restaurant.Name + ' lunchen!',
                        timeout: 3000
                    });
                })
            })
        }

        vm.leaveLunch = function (lunch) {
            //Anropa lunchen
            vm.getThisLunch = dataservice.getLunch(lunch.Id).then(function (response) {
                vm.getThisLunch.data = response;

                dataservice.updateLunch(vm.getThisLunch.data, vm.loggedIn.data).success(function (response) {
                    dataservice.getLunches().then(function (data) {
                        vm.lunchList.data = data;
                    })
                    toaster.pop({
                        toasterId: 4,
                        type: 'warning',
                        title: 'Lämnat!',
                        body: 'Du har nu lämnat ' + vm.getThisLunch.data.Restaurant.Name + ' lunchen!',
                        timeout: 3000
                    });
                })
            })
        }

        //Skapa lunch
        vm.createLunch = function (form) {

            var newLunch = {
                restaurant: form.restaurant.$modelValue,
                lunchTime: form.time.$$lastCommittedViewValue
            }
            if (newLunch.Restaurant == undefined) {
                var validationType = 'Du måste välja en restaurang';
            }
            dataservice.postLunch(newLunch, vm.loggedIn.data).success(function (xxx) {
                getLunches();
                vm.closeModal();
                toaster.pop({
                    toasterId: 3,
                    type: 'info',
                    title: 'Skapad!',
                    body: 'Lunchen ' + newLunch.restaurant.Name + ' har nu skapats!',
                    timeout: 3000
                });

            }).error(function (response) {
                toaster.pop({
                    type: 'error',
                    title: 'Fel!',
                    body: validationType,
                    timeout: 2000
                })
            })
                vm.lunchList.data.push();   
        }

        vm.removeLunch = function (index) {
            var lunchToDelete = vm.lunchList.data[index];
            dataservice.deleteLunch(lunchToDelete.Id).then(function (response) {
                vm.lunchList.data.splice(index, 1);

                toaster.pop({
                    toasterId: 1,
                    type: 'error',
                    title: 'Raderad!',
                    body: 'Lunchen raderades från listan',
                    timeout: 2000
                });
            })
        }

        vm.showDetails = function (index) {
            $("#members" + index).slideToggle(800);
        }
    }
})();
