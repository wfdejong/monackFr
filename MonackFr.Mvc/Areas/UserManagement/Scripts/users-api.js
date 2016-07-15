///
/// Factory for users api
///
angular.module('monackfr').factory('usersApi', function ($resource) {

    return $resource(
        "api/users/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
});