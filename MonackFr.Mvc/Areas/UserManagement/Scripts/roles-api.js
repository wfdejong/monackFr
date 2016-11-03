///
/// Factory for roles api
///
angular.module('monackfr').factory('rolesApi', ["$resource", function ($resource) {

    return $resource(
        "api/rolesapi/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
}]);