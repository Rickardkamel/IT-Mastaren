(function () {
    'use strict';

    angular
        .module('starter.auth')
        .factory('authInterceptorService', authInterceptorService);

    authInterceptorService.$inject = ['$q', '$location'];
    function authInterceptorService($q, $location) {
        var service = {
            request: request,
            responseError: responseError,
        };
        
        return service;
        
        function request(config) {
            config.headers = config.headers || {};
            
            var authData = JSON.parse(window.localStorage.getItem('token'));
            if(authData) {
                config.headers.Authorization = 'Bearer ' + authData.token; 
            } else {
                if ($location.path() !== '/login'); {
                    $location.path('/login');
                }
            }
            return config;
        };
        
        function responseError(rejection){
            if(rejection.status === 401){
                $location.path('/login');
            }
            return $q.reject(rejection);
        }
        ////////////////
        function exposedFn() { }
    }
})();