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

    .run(function($ionicPlatform) {
        $ionicPlatform.ready(function() {
            if (window.cordova && window.cordova.plugins.Keyboard) {
                
                cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

                cordova.plugins.Keyboard.disableScroll(true);
            }
            if (window.StatusBar) {
                StatusBar.styleDefault();
            }
        });
    })

    .config(function($stateProvider, $urlRouterProvider, $ionicConfigProvider) {


        $ionicConfigProvider.tabs.position('bottom');
        
        $stateProvider
        
            .state('tab', {
                url: '/tab',
                abstract: true,
                templateUrl: 'tabs/tabs.html',                
                controller: 'TabsController as vm'
            })


            .state('tab.news', {
                url: '/news',
                views: {
                    'tab-news': {
                        templateUrl: 'news/news.html',
                        controller: ''
                    }
                }
            })

           .state('tab.settings', {
                url: '/settings',
                views: {
                    'tab-settings': {
                        templateUrl: 'settings/settings.html',
                        controller: 'SettingsController as vm',
                    }
                }
            })
            
            .state('tab.lunch', {
                url: '/lunch',
                views: {
                    'tab-lunch': {
                        templateUrl: 'lunch/lunch.html',
                        controller: 'LunchController as vm'
                    }
                }
            })

            .state('tab.absence', {
                url: '/absence',
                views: {
                    'tab-absence': {
                        templateUrl: 'employee-absence/employeeAbsence.html',
                        controller: 'newEmpAbsCtrl as vm'
                    }
                }
            })

             .state('login', {
                url: '/login',
                templateUrl: 'login/login.html',
                controller: 'LoginController as vm'
            })
        $urlRouterProvider.otherwise('/login');
    });


