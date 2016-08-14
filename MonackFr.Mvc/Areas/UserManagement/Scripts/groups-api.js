///
/// Factory for users api
///
console.log('groudapi loaded');
angular.module('monackfr').factory('groupsApi', ["$resource", function ($resource) {
    
    return $resource(
        "api/groupsapi/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
}]);