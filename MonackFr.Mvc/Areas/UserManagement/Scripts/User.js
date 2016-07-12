///
/// Users controller
///
angular.module('monackfr').controller('usersController', function ($scope, $state, usersApi) {

    ///
    /// Loads a list of tiles
    ///
    $scope.loadUsers = function () {
        var entries = usersApi.query(function () {
            $scope.users = entries;
        });
    };

    $scope.loadUsers();

});

///
/// Factory for users api
///
angular.module('monackfr').factory('usersApi', function ($resource) {

    return $resource(
        "api/users/:id", { id: '@@id' },
        { "update": { method: "PUT" } }
    );
});