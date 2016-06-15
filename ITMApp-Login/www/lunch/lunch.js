(function () {
    'use strict';

    angular.module('starter.lunch')
        .controller('LunchController', LunchController);
    LunchController.$inject = ['$scope', '$ionicModal', 'dataservice', '$ionicSlideBoxDelegate'];
    function LunchController($scope, $ionicModal, dataservice, $ionicSlideBoxDelegate) {
        var vm = this;
        getLunches();
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


        dataservice.getRestaurants().then(function (response) {
            vm.restaurantData = response;
        })

        function getLunches() {
            vm.lunchList = dataservice.getLunches().then(function (response) {
                vm.lunchList.data = response;
                $ionicSlideBoxDelegate.update();
            })
        }

        vm.newLunch = function (lunch) {
            vm.openModal(lunch);

            vm.value = lunch;
        }

        vm.joinLunch = function (lunch) {
            var userName = getCookie(document.cookie.replace(/(?:(?:^|.*;\s*)userObject\s*\=\s*([^;]*).*$)|^.*$/, "$1"))

            function getCookie(userObject) {
                var parsedCookie = JSON.parse(userObject);
                return parsedCookie.userName;
            };
            //Hämta lunch
            vm.thisLunch = dataservice.getLunch(lunch.Id).then(function (response) {
                vm.thisLunch.data = response;

                //Hämta inloggade USER
                vm.loggedIn = dataservice.getLoggedInUser(userName).then(function (response) {
                    vm.loggedIn.data = response;

                    //Posta Lunchen
                    dataservice.postLunch(vm.thisLunch.data, vm.loggedIn.data).success(function (response) {
                        dataservice.getLunches().then(function (data) {
                            vm.lunchList.data = data;
                        })
                    })
                })
            })
        }
    }
})();
