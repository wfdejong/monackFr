///
/// Factory for users api
///
angular.module('monackfr').factory('groupsApi', function ($resource) {
    
    return $resource(
        "api/groupsapi/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
});