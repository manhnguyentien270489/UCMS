var module = angular.module('uccm.app.cform.edit');
module.controller('cformEditCtrl', function($scope, $http, $uibModal) {
    $scope.cform = {};
    $scope.selectedControl = undefined;

    $scope.toolbox = {
        allowedTypes: [],
        items: [
            {
                label: 'Section',
                controlType: 'Section',
                itemType: 'section',
                icon: 'fa fa-folder-o'
            },
            {
                label: 'TextBox',
                controlType: 'Text',
                itemType: 'toolbox-item',
                icon: 'icon text-box'
            },
            {
                label: 'TextArea',
                controlType: 'TextArea',
                itemType: 'toolbox-item',
                icon: 'icon text-area'
            },
            {
                label: 'Button',
                controlType: 'Button',
                itemType: 'toolbox-item',
                icon: 'icon button'
            },
        ]
    };

    $scope.properties = [
        {
            name: 'Form Title',
            value: 'Title'
        },
        {
            name: 'Form Description',
            value: 'Description'
        }
    ];

    $scope.$watch('selectedControl', function (newValue) {
        if (!newValue || !newValue.Type) {
            $scope.properties = [
                {
                    name: 'Form Title',
                    value: 'Title'
                },
                {
                    name: 'Form Description',
                    value: 'Description'
                }
            ];
        } else {
            switch (newValue.Type) {
                case 'Section':
                    $scope.properties = [
                        {
                            name: 'Label',
                            value: 'Label'
                        }
                    ];
                    break;
                case 'Text':
                    $scope.properties = [
                        {
                            name: 'Name',
                            value: 'Name'
                        },
                        {
                            name: 'Label',
                            value: 'Label'
                        },
                        {
                            name: 'Length',
                            value: 'Length'
                        }
                    ];
                    break;
                case 'TextArea':
                    $scope.properties = [
                        {
                            name: 'Name',
                            value: 'Name'
                        },
                        {
                            name: 'Label',
                            value: 'Label'
                        }
                    ];
                    break;
                case 'Button':
                    $scope.properties = [
                        {
                            name: 'Name',
                            value: 'Name'
                        },
                        {
                            name: 'Label',
                            value: 'Label'
                        },
                        {
                            name: 'OnClick',
                            value: 'OnClick'
                        }
                    ];
                    break;
                default:
                    break;
            }
        }
    });

    $scope.onSelected = function (control) {
        var currentValue = control.Selected;
        _.each($scope.cform.Controls, function(item) { item.Selected = false; });
        control.Selected = !currentValue;
        if (control.Selected) {
            if (control.Type === 'Section')
                $scope.selectedControl = control;
            else {
                $scope.selectedControl = control.Items[0];
            }
        } else {
            $scope.selectedControl = $scope.cform;            
        }
    }
    
    $scope.onDropItem = function (index, item, type, parent) {
        var ctrl, subItem;
        switch (type) {
        case 'section':
            ctrl = {
                Id: new Date().valueOf(),
                Type: 'Section',
                Label: item.label,
                itemType: 'control'
            };
            parent.Controls.splice(index, 0, ctrl);
            break;
        case 'toolbox-item':
            ctrl = {
                Id: new Date().valueOf(),                
                Type: 'Row',
                Label: '',
                itemType: 'control',
                Items: []
            };
            
            subItem = {
                Id: new Date().valueOf(),
                Type: item.controlType,
                Name: item.label,
                Label: item.label,
                itemType: 'toolbox-item'
            };
            ctrl.Items.push(subItem);
            parent.Controls.splice(index, 0, ctrl);
            break;
        case 'control':
            _.remove(parent.Controls, { Id: item.Id });
            parent.Controls.splice(index, 0, item);
        }
        return true;
    }

    $scope.editScript = function() {
        var modal = $uibModal.open({
            controller: 'scriptEditingModalCtrl',
            templateUrl: 'scriptEditingModal.html',
            resolve: {
                script: function() { return $scope.cform.Scripts; }
            },
            size: 'lg'
        });
        modal.result.then(function(script) {
            $scope.cform.Scripts = script;
        });
    }

    $scope.cancel = function() {
        location.href = "/form";
    }

    $scope.saveForm = function() {
        $http.post('/api/cform/save', $scope.cform).then(function() {
            alert('Save successfully');
        });
    }

    $scope.init = function() {
        $http.get('/api/cform/getbyid?id=' + Utils.getParameterByName('id')).then(function (response) {
            $scope.cform = response.data;
            if (!$scope.cform.Controls) $scope.cform.Controls = [];
            $scope.selectedControl = $scope.cform;
        });
    }
});

module.controller('scriptEditingModalCtrl', function($scope, $uibModalInstance, script) {
    $scope.cmOptions = {
        lineNumbers: true,
        matchBrackets: true,
        mode: "text/javascript",
        theme: "monokai",
        extraKeys: {
            "Tab": function (cm) {
                cm.replaceSelection("   ", "end");
            }
        },
        onLoad: function (cm) {
            window.setTimeout(function() {
                cm.refresh();
            }, 100);
        }
    };

    $scope.script = script;

    $scope.ok = function() {
        $uibModalInstance.close($scope.script);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    }
});

