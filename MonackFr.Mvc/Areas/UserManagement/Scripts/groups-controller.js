///
/// Groups controller
///
angular.module('monackfr')
    .controller('groupsController', ["groupsApi",
        function(groupsApi) {

            var groupsCtrl = this;

            ///
            /// Loads a list of users
            ///
            groupsCtrl.loadGroups = function () {
                var entries = groupsApi.query(function() {
                    groupsCtrl.groups = entries;
                });
            };

            groupsCtrl.loadGroups();
        }]);

///
/// New group controller
///
angular.module('monackfr')
    .controller('newGroupController',
    [
        "$state", "groupsApi",
        function($state, groupsApi) {
            var newGroupCtrl = this;
            newGroupCtrl.add = function() {
                groupsApi.save(newGroupCtrl.Group,
                    function() {
                        $state.go('monackfr-usermanagement-groups');
                    });
            }
        }
    ]);
