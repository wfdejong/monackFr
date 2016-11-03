///
/// Roles controller
///
angular.module('monackfr')
    .controller('rolesController', ["$scope", "$state", "rolesApi",
        function ($scope, $state, rolesApi) {
            var rolesCtrl = this;

            ///
            /// Loads a list of users
            ///
            rolesCtrl.load = function () {
                var entries = rolesApi.query(function () {
                    rolesCtrl.roles = entries;
                });
            };

            rolesCtrl.load();
        }
    ]);