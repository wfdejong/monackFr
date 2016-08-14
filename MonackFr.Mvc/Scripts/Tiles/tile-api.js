console.log('tile-api.js loaded');
///
/// Factory for tile api
///
angular.module('monackfr')
    .factory('tileApi', ["$resource",
        function ($resource) {
            return $resource(
                "api/tile/:id",
                { id: '@@id' },
                { "update": { method: "PUT" } }
            );
        }]);