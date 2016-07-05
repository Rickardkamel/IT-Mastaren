(function () {
    'use strict';

    angular
        .module('starter.settings')
        .controller('SettingsController', SettingsController);

    SettingsController.$inject = ['$state'];
    function SettingsController($state) {
        var vm = this;

        vm.logout = logout;

        // function logout() {
        //     window.localStorage.removeItem('token');
        //     $state.go('login');
        // }
        function logout() {
            var deferred = $q.defer();

            localstorageFactory.remove('user');
            $state.go('layout.login');
            deferred.resolve();

            return deferred.promise;
        }

    }
})();