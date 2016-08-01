///
/// Factory for users api
///
angular.module('monackfr').factory('usersApi', function ($resource) {

    return $resource(
        "api/usersapi/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
});