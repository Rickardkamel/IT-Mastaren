(function() {
    'use strict';

    angular
        .module('starter')
        .factory('dataservice', dataservice);

    dataservice.$inject = ['$http'];
    function dataservice($http) {
        var result;
        var services = {
            getEmployeeAbsence: getEmployeeAbsence,
            getEmployeeAbsences: getEmployeeAbsences,
            getAbsence: getAbsence,
            getAbsences: getAbsences,
            getEmployee: getEmployee,
            getEmployees: getEmployees,
            postEmployeeAbsence: postEmployeeAbsence,
            deleteEmpAbs: deleteEmpAbs,
            postEmployee: postEmployee,
            getLoggedInUser: getLoggedInUser,
            getRestaurants: getRestaurants,
            getLunches: getLunches,
            postLunch: postLunch,
            getLunch: getLunch,
            deleteLunch: deleteLunch,
        };

        var baseAdress = 'http://localhost:58054/';

        var config = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

        return services;



        ////////////////
        // EmployeeAbsence
        function getEmployeeAbsence(id) {
            return $http.get(baseAdress + 'api/EmployeeAbsence/' + id).then(function(response) {
                return response.data;
            });
        }
        function getEmployeeAbsences() {
           return  $http.get(baseAdress + 'api/EmployeeAbsence').then(function(response) {
                return response.data;
            });
        }

        function postEmployeeAbsence(data){
            return $http.post(baseAdress + 'api/EmployeeAbsence', data, config).success(function (data){
                return data;
            });
        }

        function deleteEmpAbs(id){
            return $http.delete(baseAdress + 'api/EmployeeAbsence/'+ id).success(function (response){
                return response;
            });
        }

        // Absence
        function getAbsence(id) {
            return $http.get(baseAdress + 'api/Absence/' + id).then(function(response) {
                return response.data;
            });
        }
        function getAbsences() {
            return $http.get(baseAdress + 'api/Absence').then(function(response) {
                return response.data;
            });
        }

        // Employee
        function getEmployee(id) {
            return $http.get(baseAdress + 'api/Employee/' + id).then(function(response) {
                return response.data;
            });
        }
        function getLoggedInUser(userName) {
            return $http.get(baseAdress + 'api/Employee/' + userName).then(function(response) {
                return response.data;
            });
        }

        function getEmployees() {
            return $http.get(baseAdress + 'api/Employee').then(function(response) {
                return response.data;
            });
        }
        function postEmployee(data){
            return $http.post(baseAdress + 'api/Employee', data, config).success(function (data){
                return data;
            });
        }
        function getRestaurants(){
            return $http.get(baseAdress + 'api/restaurant').then(function(response){
                return response.data;
            })
        }
        function postLunch(data1, data2){
            var inData = {'lunch': data1, 'employee': data2}
            return $http.post(baseAdress + 'api/lunch', inData).success(function(response){
                return response;
            })
        }
        function getLunches(){
            return $http.get(baseAdress + 'api/lunch').then(function(response){
                return response.data;
            })
        }
        function getLunch(id) {
            return $http.get(baseAdress + 'api/lunch/' + id).then(function(response) {
                return response.data;
            });
        }
        function deleteLunch(id){
            return $http.delete(baseAdress + 'api/lunch/'+ id).success(function (response){
                return response;
            });
        }
    }
})();