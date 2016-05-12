(function () {
    'use strict';

    angular
        .module('starter.login')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['authService', '$http', 'dataservice', 'toaster'];
    function LoginController(authService, $http, dataservice, toaster) {
        var vm = this;

        vm.message = "";
        vm.loginForm = {
            userName: "",
            password: ""
        };

        vm.login = function () {

            authService.login(vm.loginForm).then(function (response) {

                vm.createEmployee(response);
            }, function (error) {

                if (error == null) {
                    toaster.pop({
                        toasterId: 4,
                        type: 'info',
                        title: 'Fel!',
                        body: 'Fel vid anslutningen',
                        timeout: 3000
                    });
                    return;
                }

                toaster.pop({
                    toasterId: 4,
                    type: 'error',
                    title: 'Fel!',
                    body: 'Fel användarnamn/lösenord',
                    timeout: 3000
                })
            });
        };

        vm.createEmployee = function (response) {
            var newEmployee = {
                Name: response.displayName,
                UserName: response.userName,
                Email: response.email
            }
            dataservice.postEmployee(newEmployee);
        }
    }
})();