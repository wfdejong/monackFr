///
/// Factory for tile api
///
angular.module('monackfr')
    .factory('tileApi',
        function ($resource) {
            return $resource(
                "api/tile/:id",
                { id: '@@id' },
                { "update": { method: "PUT" } }
            );
        });