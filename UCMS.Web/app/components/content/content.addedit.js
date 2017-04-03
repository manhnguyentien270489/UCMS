var module = angular.module('uccm.app.content.addedit');

module.controller('contentAddEditCtrl', function($scope, $xhttp) {
    $scope.type = 'content';

    $scope.content = {
        Id: '',
        Name: '',
        ContentType: {},
        Values: {}
    };

    $scope.contentTypes = [];

    $scope.validateForm = function(form) {
        if (form.$invalid) return false;
        if ($scope.content.ContentType.Id === 'dummy') return false;
        return true;
    }

    $scope.submit = function() {
        var url = '/api/';
        var successMsg;
        if ($scope.type === 'list') {
            url += '/datalist';
            successMsg = ' DataList successfull';
        } else {
            url += '/content';
            successMsg = ' Content successfull';
        }
        var id = Utils.getParameterByName("id");
        if (id) {
            url += '/update';
            successMsg = 'Update ' + successMsg;
        } else {
            url += '/create';
            successMsg = 'Create ' + successMsg;
        }

        $xhttp.post(url, $scope.content).then(function() {
            $scope.alertSvc.addSuccess(successMsg);
            setTimeout(function() {
                $scope.cancel();
            }, 1000);
        });
    }

    $scope.cancel = function() {
        if ($scope.type === 'list') {
            location.href = '/datalist?type=' + $scope.content.ContentType.Id;
        } else {
            location.href = '/fs/folder?id=' + $scope.content.Folder.Id;
        }
    }

    $scope.init = function () {
        var id = Utils.getParameterByName("id");

        if (location.href.toLowerCase().indexOf('/datalist') >= 0) {
            $scope.type = 'list';
        } else {
            $scope.type = 'content';
        }

        $xhttp.get('/api/contenttype/getall').then(function(response) {
            $scope.contentTypes = _.filter(response.data, {IsDataList: false});
            var dummyType = {
                Id: 'dummy',
                Name: '---- None ----',
                Description: '',
                Fields: []
            };
            $scope.contentTypes.splice(0, 0, dummyType);

            if (!id) {
                if ($scope.type === 'list') {
                    var typeId = Utils.getParameterByName('type');
                    $scope.content.ContentType = _.find($scope.contentTypes, { Id: typeId });
                    $scope.content.Name = $scope.content.ContentType.Name;
                } else {
                    $scope.content.ContentType = $scope.contentTypes[0];
                    var folderId = Utils.getParameterByName('folderId');
                    $xhttp.get('/api/folder/getfolder?id=' + folderId).then(function(res) {
                        $scope.content.Folder = res.data;
                    });
                }                
            } else {
                var url;
                if ($scope.type === 'list') {
                    url = '/api/datalist/getbyid?id=' + id;
                } else {
                    url = '/api/content/getbyid?id=' + id;
                }

                $xhttp.get(url).then(function(response) {
                    $scope.content = response.data;
                });
            }
        });
    }
});