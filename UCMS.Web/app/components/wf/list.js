app.controller("wfListCtrl", function ($scope, $xhttp, $modal) {
    $scope.fields = ["Name", "Description"];
    $scope.init = function () {
        $xhttp.get('/api/WF/GetWorkflows').then(function (response) {
            $scope.workflows = response.data;
        });
    }
    
    $scope.edit = function (wf) {
        window.location = "/wf/detail?id=" + wf.Id;
    }
})