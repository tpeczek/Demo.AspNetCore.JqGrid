var demo = (function () {
    var terrainFlags = [
        { flag: 1, description: 'Desert' },
        { flag: 2, description: 'Plains' },
        { flag: 4, description: 'Grass' },
        { flag: 8, description: 'Grasslands' },
        { flag: 16, description: 'Grassy Hills' },
        { flag: 32, description: 'Hills' },
        { flag: 64, description: 'Swamps' },
        { flag: 128, description: 'Bogs' },
        { flag: 256, description: 'Forests' },
        { flag: 512, description: 'Jungle' },
        { flag: 1024, description: 'Lakes' },
        { flag: 2048, description: 'Rivers' },
        { flag: 4096, description: 'Oceans' },
        { flag: 8192, description: 'Rocky Islands' },
        { flag: 16384, description: 'Mountains' },
        { flag: 32768, description: 'Cityscape' },
        { flag: 65536, description: 'Urban' }
    ];

    var appendFlagDescription = function (value, flag, valueDescription, flagDescription) {
        if ((value & flag) !== 0) {
            if (valueDescription !== '') {
                valueDescription += ', ';
            }

            valueDescription += flagDescription;
        }

        return valueDescription;
    };

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

                    if (hairColorDescription === null) {
                        hairColorDescription = 'Unknown';
                    }
                    else if (!isNaN(cellvalue)) {
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
            planet: {
                climateFormatter: function (cellvalue, options, rowObject) {
                    var climateDescription = cellvalue;

                    if (!isNaN(cellvalue)) {
                        var climateValue = parseInt(cellvalue);

                        switch (climateValue) {
                            case 1:
                                climateDescription = 'Arid';
                                break;
                            case 2:
                                climateDescription = 'Temperate';
                                break;
                            case 3:
                                climateDescription = 'Tropical';
                                break;
                            case 4:
                                climateDescription = 'Hot';
                                break;
                            case 5:
                                climateDescription = 'Polluted';
                                break;
                            default:
                                climateDescription = '';
                                break;
                        }
                    }

                    return climateDescription;
                },
                terrainFormatter: function (cellvalue, options, rowObject) {
                    var terrainDescription = cellvalue;

                    if (!isNaN(cellvalue)) {
                        terrainDescription = '';
                        var terrainValue = parseInt(cellvalue);

                        for (var i = 0; i < terrainFlags.length; i++) {
                            terrainDescription = appendFlagDescription(terrainValue, terrainFlags[i].flag, terrainDescription, terrainFlags[i].description);
                        }
                    }

                    return terrainDescription;
                }
            },
            nullAsUnknownFormatter: function (cellvalue, options, rowObject) {
                if (cellvalue === null) {
                    cellvalue = 'Unknown';
                }

                return cellvalue;
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