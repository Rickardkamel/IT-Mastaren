(function () {
    'use strict';

    angular.module('directives.module', [])
        .directive('swiper', function () {
            return {
                link: function (scope, element, attr) {
                    //Option 1 - on ng-repeat change notification
                    scope.$on('content-changed', function () {

                        new Swiper('.swiper-container', {
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
                    });

                }
            };
        })
        .directive('isLoaded', function () {

            return {
                scope: false, //don't need a new scope
                restrict: 'A', //Attribute type
                link: function (scope, elements) {

                    if (scope.$last) {
                        scope.$emit('content-changed');
                        console.log('page Is Ready!');
                    }
                }
            }
        })
})();





