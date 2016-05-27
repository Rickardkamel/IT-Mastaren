(function () {
    'use strict';

    angular
        .module('starter.tabs')
        .controller('TabsController', TabsController);

    TabsController.$inject = ['$state'];
    function TabsController($state) {
        var vm = this;

        vm.loggedIn = false;





        activate();

        ////////////////

        function activate() {

            isLoggedIn()


            function isLoggedIn() {
                var authData = JSON.parse(window.localStorage.getItem('token'));

                if (authData) {
                    vm.loggedIn = true;
                }
                else {
                    vm.loggedIn = false;
                    $state.go('login')
                }
            }
        }
    }
})();