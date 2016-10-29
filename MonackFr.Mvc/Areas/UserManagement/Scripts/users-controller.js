///
/// Users controller
///
angular.module('monackfr')
    .controller('usersController',
    [
        "$scope", "$state", "usersApi",
        function($scope, $state, usersApi) {
            console.log('usercontroller loaded');
            var ctrl = this;

            ///
            /// Loads a list of users
            ///


            ctrl.loadUsers = function() {
                var entries = usersApi.query(function() {
                    ctrl.users = entries;
                });
            };

            ctrl.loadUsers();

        }
    ]);
angular.module('monackfr')
    .controller('newUserController',
    [
        "$scope", "$state", "usersApi",
        function($scope, $state, usersApi) {
            console.log('newuserscontroller loaded');
            $scope.addUser = function() {
                console.log("newuser: ", usersApi);
            }

        }
    ]);
       