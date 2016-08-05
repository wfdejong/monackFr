///
/// Users controller
///
angular.module('monackfr').controller('usersController', function ($scope, $state, usersApi) {

    var ctrl = this;

    ///
    /// Loads a list of users
    ///
    

    ctrl.loadUsers = function () {
        var entries = usersApi.query(function () {
            ctrl.users = entries;
        });
    };

    ctrl.loadUsers();

})
