///
/// Users controller
///
angular.module('monackfr')
    .controller('usersController', ["usersApi",
        function (usersApi) { 
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
    .controller('newUserController', ["$state", "usersApi",
        function ($state, usersApi) {
            var newUserCtrl = this;
            newUserCtrl.add = function () {
                usersApi.save(newUserCtrl.user, function () {
                    $state.go('monackfr-usermanagement-users');
                });
            }
        }
    ]);

///
/// Edit user controller
///
angular.module('monackfr')
.controller('editUserController', ["$state", "$stateParams", "usersApi",
   function ($state, $stateParams, usersApi) {
       var editUserCtrl = this;
       editUserCtrl.loadUser = function () {
           editUserCtrl.user = usersApi.get({ id: $stateParams.id });
       }

       editUserCtrl.edit = function () {
           editUserCtrl.user.$update(function () {
               $state.go('monackfr-usermanagement-users');
           });
       }

       editUserCtrl.loadUser();
   }
]);