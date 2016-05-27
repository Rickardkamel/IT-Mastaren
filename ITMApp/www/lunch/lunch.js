(function () {
    'use strict';

    angular
        .module('starter.lunch')
        .controller('LunchController', LunchController);

    LunchController.$inject = ['$scope', 'dataservice'];
    function LunchController($scope, dataservice) {
        var vm = this;
        getLunches();
        var swiper = new Swiper('.swiper-container', {
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
        
        function getLunches(){
            vm.lunchList = dataservice.getLunches().then(function (response){
                vm.lunchList.data = response;
            })
        }



        activate();

        ////////////////

        function activate() { }
    }
})();