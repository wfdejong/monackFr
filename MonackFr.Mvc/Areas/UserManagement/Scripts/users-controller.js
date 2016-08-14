///
/// Users controller
///
console.log('usercontroller 5 loaded');
angular.module('monackfr')
    .controller('usersController', ["$scope", "$state", "usersApi",
        function($scope, $state, usersApi) {

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

        }]);
