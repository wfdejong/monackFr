console.log('monackfr.js loaded');
var monackfr = angular.module('monackfr', [
    'ui.router',
    'ngResource'
]);

///
/// config for router 
///
monackfr.config([
    '$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/tiles');

        $stateProvider
            .state('tiles',
            {
                url: '/tiles',
                templateUrl: 'main/tile',
                controller: 'tile-controller',
                controllerAs: 'tile'
            })
            .state('monackfr-usermanagement-users',
            {
                url: '/module/usermanagement',
                templateUrl: 'usermanagement/user/index',
                controller: 'usersController',
                controllerAs: 'usersController'
            })
            .state('monackfr-usermanagement-groups',
            {
                url: '/module/usermanagement/groups',
                templateUrl: 'usermanagement/group/index',
                controller: 'groupsController',
                controllerAs: 'groupsController'
            })
            .state('monackfr-usermanagement-roles',
            {
                url: '/module/usermanagement/roles',
                templateUrl: 'usermanagement/role/index',
                controller: 'usersController',
                controllerAs: 'usersController'
            });
    }
]);