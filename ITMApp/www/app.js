// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
angular.module('starter', [
    'ionic',
    'toaster',
    'ngAnimate',
    'starter.newEmpAbs',
    'starter.auth',
    'starter.login',
    'starter.tabs',
    'starter.settings',
    'starter.lunch',
    'directives.module',

])

    .run(function ($state, $rootScope, $ionicPlatform) {
        $ionicPlatform.ready(function () {
            if (window.cordova && window.cordova.plugins.Keyboard) {

                cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

                cordova.plugins.Keyboard.disableScroll(true);
            }
            if (window.StatusBar) {
                StatusBar.styleDefault();
            }
        });

        $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
            if (error === "Not Authorized") {
                $state.go("login");
            }
        });
    })



    .config(function ($httpProvider, $stateProvider, $urlRouterProvider, $ionicConfigProvider) {

        var isLoggedIn = {
            security: ['$q', 'authService', function ($q, authService) {
                if (!authService.checkLoggedInStatus()) {
                    return $q.reject("Not Authorized");
                }
            }]
        };

        $httpProvider.interceptors.push('authInterceptorService');
        $httpProvider.defaults.headers.post = { 'Content-Type': 'application/json' };

        $ionicConfigProvider.tabs.position('bottom');

        $stateProvider

            .state('tab', {
                url: '/',
                abstract: true,
                templateUrl: 'tabs/tabs.html',
                controller: 'TabsController as vm',
            })

            .state('login', {
                url: '/login',
                templateUrl: 'login/login.html',
                controller: 'LoginController as vm'
            })

            .state('tab.news', {
                url: 'news',
                views: {
                    'tab-news': {
                        templateUrl: 'news/news.html',
                        controller: '',
                        resolve: isLoggedIn
                    }
                }
            })

            .state('tab.settings', {
                url: 'settings',
                views: {
                    'tab-settings': {
                        templateUrl: 'settings/settings.html',
                        controller: 'SettingsController as vm',
                        resolve: isLoggedIn
                    }
                }
            })

            .state('tab.lunch', {
                url: 'tab/lunch',
                views: {
                    'tab-lunch': {
                        templateUrl: 'lunch/lunch.html',
                        controller: 'LunchController as vm',
                        resolve: isLoggedIn
                    }
                }
            })

            .state('tab.absence', {
                url: 'absence',
                views: {
                    'tab-absence': {
                        templateUrl: 'employee-absence/employeeAbsence.html',
                        controller: 'newEmpAbsCtrl as vm',
                        resolve: isLoggedIn
                    }
                }
            })


        $urlRouterProvider.otherwise(window.localStorage.getItem('token') == null ? '/tab/login' : 'tab/lunch');
    });

