(function () {
    'use strict';

    angular.module('starter.lunch')
        .controller('LunchController', LunchController);
    LunchController.$inject = ['$scope', '$ionicModal', 'dataservice', '$ionicSlideBoxDelegate'];
    function LunchController($scope, $ionicModal, dataservice, $ionicSlideBoxDelegate) {
        var vm = this;
        getLunches();

        // Datum-koll
        vm.dateCheck = function(date){
            date = new Date(date).getDate();
            var today = new Date().getDate();
            if(date == today){
                return true;
            }
            if(date !== today){
                return false;
            }
        }

        // Modal-Instance
        $ionicModal.fromTemplateUrl('newlunch-modal.html', function (modal) {
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

        //Join lunch
        vm.joinLunch = function (lunch) {

            //Hämta lunch
            vm.thisLunch = dataservice.getLunch(lunch.Id).then(function (response) {
                vm.thisLunch.data = response;
                //Posta Lunchen
                dataservice.postLunch(vm.thisLunch.data, vm.loggedIn.data).success(function (response) {
                    dataservice.getLunches().then(function (data) {
                        vm.lunchList.data = data;
                    })
                })
            })
        }

        //Skapa lunch
        vm.createLunch = function (form) {

            var newLunch = {
                restaurant: form.restaurant.$modelValue,
                lunchTime: form.time.$$lastCommittedViewValue
            }

            dataservice.postLunch(newLunch, vm.loggedIn.data).then(function (xxx) {
                getLunches();

            })
            vm.lunchList.data.push();
        }

        vm.removeLunch = function (index) {
            var lunchToDelete = vm.lunchList.data[index];
            dataservice.deleteLunch(lunchToDelete.Id).then(function (response) {
                vm.lunchList.data.splice(index, 1);

                // toaster.pop({
                //     toasterId: 1,
                //     type: 'error',
                //     title: 'Raderad!',
                //     body: 'Lunchen raderades från listan',
                //     timeout: 2000
                // });
            })
        }

        vm.showDetails = function (index) {
            $("#members" + index).slideToggle(800);
        }

    }
})();
