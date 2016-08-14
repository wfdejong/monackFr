///
/// Groups controller
///
console.log('groupcontroller loaded');
angular.module('monackfr')
    .controller('groupsController', ["$scope", "$state", "groupsApi",
        function($scope, $state, groupsApi) {

            var ctrl = this;

            ///
            /// Loads a list of users
            ///


            ctrl.loadGroups = function() {
                var entries = groupsApi.query(function() {
                    ctrl.groups = entries;
                });
            };

            ctrl.loadGroups();
        }]);
