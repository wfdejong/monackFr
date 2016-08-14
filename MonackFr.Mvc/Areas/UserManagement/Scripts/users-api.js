///
/// Factory for users api
///
console.log('userapi loaded');
angular.module('monackfr').factory('usersApi', ["$resource", function ($resource) {

    return $resource(
        "api/usersapi/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
}]);