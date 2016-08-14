console.log('tile-controller.js loaded');
angular.module('monackfr')
    .controller('tile-controller', ["$scope", "$state", "tileApi",
        function($scope, $state, tileApi) {

            var ctrl = this;

            ///
            /// Loads a list of tiles
            ///
            ctrl.loadTiles = function() {
                ctrl.tiles = tileApi.query(function() {
                });
            };

            ctrl.loadTiles();
        }]);

