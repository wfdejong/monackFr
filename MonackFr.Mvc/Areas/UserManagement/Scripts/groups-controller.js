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
            groupsCtrl.load = function () {
                var entries = groupsApi.query(function() {
                    groupsCtrl.groups = entries;
                });
            };

            ///
            /// Deletes group
            groupsCtrl.delete = function (group) {
                var params = { id: group.Id };
                group.$delete(params, function () {
                    groupsCtrl.load();
                });
            };

            groupsCtrl.load();
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

///
/// Edit user controller
///
angular.module('monackfr')
.controller('editGroupController', ["$state", "$stateParams", "groupsApi",
   function ($state, $stateParams, groupsApi) {
       var editGroupCtrl = this;
       editGroupCtrl.loadGroup = function () {
           editGroupCtrl.group = groupsApi.get({ id: $stateParams.id });
       }

       editGroupCtrl.edit = function () {
           editGroupCtrl.group.$update(function () {
               $state.go('monackfr-usermanagement-groups');
           });
       }

       editGroupCtrl.loadGroup();
   }
]);
