var demo = (function() { 

    return {
        jqGrid: {
            character: {
                genderFormatter: function (cellvalue, options, rowObject) {
                    var genderDescription = '';

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
                        }
                    }

                    return genderDescription;
                },
                skinColorFormatter: function (cellvalue, options, rowObject) {
                    var skinColorDescription = '';

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
                        }
                    }

                    return skinColorDescription;
                },
                hairColorFormatter: function (cellvalue, options, rowObject) {
                    var hairColorDescription = '';

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
                        }
                    }

                    return hairColorDescription;
                },
                eyeColorFormatter: function (cellvalue, options, rowObject) {
                    var eyeColorDescription = '';

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
                        }
                    }

                    return eyeColorDescription;
                },
                eyeColorUnFormatter: function (cellvalue, options, rowObject) {
                    var eyeColor = '';

                    switch (eyeColorValue) {
                        case 'Blue':
                            eyeColor = 1;
                            break;
                        case 'Brown':
                            eyeColor = 2;
                            break;
                        case 'Yellow':
                            eyeColor = 3;
                            break;
                        case 'Hazel':
                            eyeColor = 4;
                            break;
                        case 'Red':
                            eyeColor = 5;
                            break;
                        case 'Black':
                            eyeColor = 6;
                            break;
                        case 'Orange':
                            eyeColor = 7;
                            break;
                    }

                    return eyeColor;
                }
            }
        }
    };
})();