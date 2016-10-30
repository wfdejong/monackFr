///
/// Users controller
///
angular.module('monackfr')
    .controller('usersController', ["$scope", "$state", "usersApi",
        function ($scope, $state, usersApi) {
            console.log('usercontroller loaded');
            var usersCtrl = this;

            ///
            /// Loads a list of users
            ///
            usersCtrl.load = function () {
                var entries = usersApi.query(function () {
                    usersCtrl.users = entries;
                });
            };

            ///
            /// Deletes user
            usersCtrl.delete = function (user) {
                var params = { id: user.Id };
                user.$delete(params, function () {
                    usersCtrl.load();
                });
            };

            usersCtrl.load();
        }
    ]);

///
/// New user controller
///
angular.module('monackfr')
    .controller('newUserController', ["$scope", "$state", "usersApi",
        function ($scope, $state, usersApi) {
            var newUserCtrl = this;
            newUserCtrl.add = function () {
                usersApi.save($scope.User, function () {
                    $state.go('monackfr-usermanagement-users');
                });
            }
        }
    ]);
       