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

})
