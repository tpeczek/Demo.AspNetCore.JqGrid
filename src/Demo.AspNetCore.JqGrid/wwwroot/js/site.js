var demo = (function() { 

    return {
        jqGrid: {
            character: {
                genderFormatter: function(cellvalue, options, rowObject) {
                    var genderDescription = cellvalue;

                    if (!isNaN(cellvalue)) {
                        var genderValue = parseInt(cellvalue);

                        switch (genderValue)
                        {
                            case 1:
                                genderDescription = 'Female';
                                break;
                            case 2:
                                genderDescription = 'Male';
                                break;
                            case 3:
                                genderDescription = 'Hermaphrodite';
                                break;
                            default:
                                genderDescription = '';
                                break;
                        }
                    }

                    return genderDescription;
                },
                skinColorFormatter: function(cellvalue, options, rowObject) {
                    var skinColorDescription = cellvalue;

                    if (!isNaN(cellvalue)) {
                        var skinColorValue = parseInt(cellvalue);

                        switch (skinColorValue) {
                            case 1:
                                skinColorDescription = 'Light';
                                break;
                            case 2:
                                skinColorDescription = 'Fair';
                                break;
                            case 3:
                                skinColorDescription = 'Pale';
                                break;
                            case 4:
                                skinColorDescription = 'White';
                                break;
                            case 5:
                                skinColorDescription = 'Gold';
                                break;
                            case 6:
                                skinColorDescription = 'Blue';
                                break;
                            case 7:
                                skinColorDescription = 'Red';
                                break;
                            case 8:
                                skinColorDescription = 'Green';
                                break;
                            case 9:
                                skinColorDescription = 'Green-Tan';
                                break;
                            default:
                                skinColorDescription = '';
                                break;
                        }
                    }

                    return skinColorDescription;
                },
                hairColorFormatter: function(cellvalue, options, rowObject) {
                    var hairColorDescription = cellvalue;

                    if (!isNaN(cellvalue)) {
                        var hairColorValue = parseInt(cellvalue);

                        switch (hairColorValue) {
                            case 0:
                                hairColorDescription = 'None';
                                break;
                            case 1:
                                hairColorDescription = 'Blond';
                                break;
                            case 2:
                                hairColorDescription = 'Brown';
                                break;
                            case 3:
                                hairColorDescription = 'Black';
                                break;
                            case 4:
                                hairColorDescription = 'Auburn';
                                break;
                            case 5:
                                hairColorDescription = 'Grey';
                                break;
                            case 6:
                                hairColorDescription = 'White';
                                break;
                            case 6:
                                hairColorDescription = '';
                                break;
                        }
                    }

                    return hairColorDescription;
                },
                eyeColorFormatter: function(cellvalue, options, rowObject) {
                    var eyeColorDescription = cellvalue;

                    if (!isNaN(cellvalue)) {
                        var eyeColorValue = parseInt(cellvalue);

                        switch (eyeColorValue) {
                            case 1:
                                eyeColorDescription = 'Blue';
                                break;
                            case 2:
                                eyeColorDescription = 'Brown';
                                break;
                            case 3:
                                eyeColorDescription = 'Yellow';
                                break;
                            case 4:
                                eyeColorDescription = 'Hazel';
                                break;
                            case 5:
                                eyeColorDescription = 'Red';
                                break;
                            case 6:
                                eyeColorDescription = 'Black';
                                break;
                            case 7:
                                eyeColorDescription = 'Orange';
                                break;
                            default:
                                eyeColorDescription = '';
                                break;
                        }
                    }

                    return eyeColorDescription;
                }
            },
            onJqGridInlineSuccessSaveRow: function(e, jqXHR, rowId, options) {
                var response = JSON.parse(jqXHR.responseText);

                return [response.Status, null];
            },
            onJqGridInlineAfterSaveRow: function(e, rowId, jqXHR, data, otions) {
                var response = JSON.parse(jqXHR.responseText);

                if (response.Status) {
                    $('#' + rowId).attr('id', response.CharacterId);
                }
            }
        }
    };
})();