(function () {
    'use strict';

    angular
        .module('starter.auth')
        .factory('authService', authService);

    authService.$inject = ['$location', '$http', '$q', '$state'];
    function authService($location, $http, $q, $state) {

        var serviceBase = 'http://localhost:58054/'
        var services = {
            login: login
        };
        
        
        return services;

        // Login logic
        function login(loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();            
             
            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                window.localStorage.setItem('token', angular.toJson({ token: response.access_token }));
                
                setCookie(response);
                function setCookie(response) {
                    var d = new Date();
                    d.setTime(d.getTime() + (1000));
                    var expires = 'expires=' + d.toUTCString();
                    var userInfo = {
                        userName: response.userName,
                        email: response.email,
                        displayName: response.displayName,
                    }
                    document.cookie = JSON.stringify(userInfo);
                    // document.cookie = 'token' + angular.toJson({ token: response.access_token}); 
                };
                
                // getCookie();
                // function getCookie(cookieGoingOut) {
                //   var userObj = response;
                //  };
                // window.localStorage['userName'] = response.userName;
                // window.localStorage['email'] = response.email ;               
                // window.localStorage['displayName'] = response.displayName;                                              

                $location.path('/tab/absence');
                // window.location.reload();

                deferred.resolve(response);
            }).error(function (err, status) {
                deferred.reject(err);
            });

            return deferred.promise;
        }
    }
})();