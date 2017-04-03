var module = angular.module('uccm.app.cform.show');

module.controller('cformShowCtrl', function($scope, $http, $ocLazyLoad, $compile) {

    $scope.cform = {};
    $scope.templateSrc = '';

    $scope.init = function () {
        $ocLazyLoad.toggleWatch(true);
        $http.get('/api/cform/getbyid?id=' + Utils.getParameterByName('id')).then(function(response) {
            $scope.cform = response.data;
            var js = {
                type: 'js',
                path: '/app/components/custom-form/' + $scope.cform.Id + '/script.js'
            }
            $ocLazyLoad.load(js).then(function() {
                $scope.templateSrc = '/app/components/custom-form/' + $scope.cform.Id + '/view.html';
            });
        });
    }

});

module.directive('myButton', function () {
    return {
        restrict: 'E',
        scope: true,            
        link: function (scope, element, attrs) {
            var button = angular.element("<button></button>");
            button.text(attrs.label);
            button.addClass('btn blue');
            element.replaceWith(button);
            button.bind('click', function(e) {
                scope.$apply(attrs.click);
            });
        }
    }
});