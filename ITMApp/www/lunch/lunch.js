(function () {
    'use strict';

    angular
        .module('starter.lunch')
        .controller('LunchController', LunchController);

    LunchController.$inject = ['$scope', 'dataservice'];
    function LunchController($scope, dataservice) {
        var vm = this;

        getLunches();


        function getLunches() {
            vm.lunchList = dataservice.getLunches().then(function (response) {
                vm.lunchList.data = response;

                var cubeSwiper = new Swiper('.swiper-container', {
                    pagination: '.swiper-pagination',
                    effect: 'cube',
                    grabCursor: true,
                    cube: {
                        shadow: true,
                        slideShadows: true,
                        shadowOffset: 20,
                        shadowScale: 0.94
                    }
                });

                var flipSwiper = new Swiper('.swiper-container-flip', {
                    pagination: '.swiper-pagination',
                    effect: 'flip',
                    grabCursor: true,
                    nextButton: '.swiper-button-next',
                    prevButton: '.swiper-button-prev'
                });
            })
        }


        vm.joinLunch = function (lunchId) {
            var userName = getCookie(document.cookie.replace(/(?:(?:^|.*;\s*)userObject\s*\=\s*([^;]*).*$)|^.*$/, "$1"))

            function getCookie(userObject) {
                var parsedCookie = JSON.parse(userObject);
                return parsedCookie.userName;
            };

            vm.thisLunch = dataservice.getLunch(lunchId).then(function (response) {
                vm.thisLunch.data = response;


                vm.loggedIn = dataservice.getLoggedInUser(userName).then(function (response) {
                    vm.loggedIn.data = response;

                    vm.thisLunch.data.EmployeesList.push(vm.loggedIn)

                    dataservice.postLunch(vm.thisLunch.data).then(function (response) {
                        getLunches();
                    })
                })
            })
        }




        activate();

        ////////////////

        function activate() { }
    }
})();