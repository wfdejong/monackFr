
var monackfr = angular.module('monackfr', [
    'ui.router',
    'ngResource'
]);

///
/// config for router 
///
monackfr.config([
    '$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/tiles');

        $stateProvider
            .state('tiles',
            {
                url: '/tiles',
                templateUrl: 'Scripts/Tiles/tile.tmpl.html',
                controller: 'tile-controller',
                controllerAs: 'tile'
            })
            .state('monackfr-usermanagement',
            {
                url: '/module/monackfr-usermanagement',
                templateUrl: 'usermanagement/user/index',
                controller: 'usersController'
            });
    }
]);