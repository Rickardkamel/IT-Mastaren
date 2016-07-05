(function () {
    'use strict';

    angular
        .module('starter.settings')
        .controller('SettingsController', SettingsController);

    SettingsController.$inject = ['$q', '$state'];
    function SettingsController($q, $state) {
        var vm = this;

        vm.logout = logout;

        // function logout() {
        //     window.localStorage.removeItem('token');
        //     $state.go('login');
        // }
        function logout() {
            var deferred = $q.defer();

            window.localStorage.removeItem('token');
            $state.go('login');
            deferred.resolve();

            return deferred.promise;
        }

    }
})();