angular.module('uccm.app.core',
[
]);

/* CONTENT TYPE MODULES */
angular.module('uccm.app.contentType.list',
[    
]);

angular.module('uccm.app.contentType.addedit',
[
    'ui.bootstrap'
]);

angular.module('uccm.app.contentType',
[
    'uccm.app.contentType.list',
    'uccm.app.contentType.addedit'
]);

/* DATA LIST MODULES */
angular.module('uccm.app.datalist.list',
[]);
angular.module('uccm.app.datalist',
[
    'uccm.app.datalist.list',
]);

/* CONTENT MODULES */
angular.module('uccm.app.content.addedit',
[]);

angular.module('uccm.app.content',
[
    'uccm.app.content.addedit'
]);

/* FS MODULES */

angular.module('uccm.app.fs.library',
[]);
angular.module('uccm.app.fs.folder',
[]);

angular.module('uccm.app.fs',
[
    'uccm.app.fs.library',
    'uccm.app.fs.folder'
]);

angular.module('uccm.app.wf.detail',
[
]);

var app = angular.module('uccm.app', [
    'ngHttp',
    'ngAlert',
    'icheck.directives',
    'nya.bootstrap.select',
    'uccm.modal',
    'uccm.app.core',
    'uccm.app.contentType',
    'uccm.app.datalist',
    'uccm.app.content',
    'uccm.app.fs',
    'uccm.app.wf.detail',
    'ngRoute'
])
.config(function ($routeProvider, $locationProvider) {
    // configure the routing rules here
    $routeProvider.when('/wf/detail/:id', {
        controller: 'wfDetailCtrl'
    });

    // enable HTML5mode to disable hashbang urls
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});


app.run(function($rootScope, alertSvc) {
    $rootScope.alertSvc = alertSvc;
    $rootScope.hasError = function (field, errorType) {
        switch (errorType) {
            case 'required':
                return field.$dirty && field.$error.required;
            case 'pattern':
                return field.$dirty && field.$error.pattern;
            case 'invalid':
                return field.$dirty && field.$invalid;
            default:
                return false;
        }
    }
});

