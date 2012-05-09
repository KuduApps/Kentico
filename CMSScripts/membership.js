// Shows password strength
function ShowStrength(passwordID, minLength, preferedLength, minNonAlphaNumChars, preferedNonAlphaNumChars, regularExpression, passwordLabelID, policyStrings, classPrefix, usePolicy, indicatorPanelID,  useStylesForStrenghtIndicator) {
   var strings = policyStrings.split(';');

    var passElem = document.getElementById(passwordID);
    var labelElem = document.getElementById(passwordLabelID);
    var indicatorElem = document.getElementById(indicatorPanelID);
    
    if (passElem && labelElem) {
        
        var passwordValue = passElem.value;
        var passwordLength = passwordValue.length ;

        if (usePolicy == 'True') {
            labelElem.setAttribute('class', classPrefix + 'NotAcceptable');
            indicatorElem.setAttribute('class', 'PasswIndicatorNotAcceptable');
            indicatorElem.setAttribute('style', 'width:0px;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'width:0px;';
        }
        else {
            labelElem.setAttribute('class', classPrefix + 'Weak');
            indicatorElem.setAttribute('class', 'PasswIndicatorWeak');
            indicatorElem.setAttribute('style', 'height:5px;width:20%;background-color: #ff2631;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'height:5px;width:20%;background-color: #ff2631;';
        }

        // Minimal length
        if (minLength) {
            if (passwordLength == 0) {
                labelElem.innerHTML = '';
                indicatorElem.setAttribute('class', '');
                indicatorElem.setAttribute('style', 'width:0px;');
                // Special handling for IE7
                indicatorElem.style.cssText = 'width:0px;';
                return
            }
            else if (passwordLength < parseInt(minLength)) {
                labelElem.innerHTML = strings[0];                
                return ;                            
            }
        }


        // Number of non alpha characters        
        var nonAlphaNum = 0;

        if (minNonAlphaNumChars) {

            // Count number of non alfa num characters
            for (var i = 0; i < passwordLength; i++) {
                if (!isAlphaNum(passwordValue[i])) {
                    nonAlphaNum++;
                }
            }

            if ((usePolicy == 'True') && (nonAlphaNum < parseInt(minNonAlphaNumChars))) {
                labelElem.innerHTML = strings[0];                              
                return;
            }
        }


        // Test regular expressions
        if (regularExpression) {
            var re = new RegExp(regularExpression);            
            if (!re.test(passwordValue)) {
                labelElem.innerHTML = strings[0];                
                return;
            }
        }      

        // Count result strength        
        var onePercent = preferedLength / 100.0;
        var lenghtPercent = passwordLength / onePercent;

        onePercent = preferedNonAlphaNumChars / 100.0;
        var nonAlphaPercent = nonAlphaNum / 100.0;

        //alert(lenghtPercent+';'+nonAlphaPercent);
        var percentResult = (lenghtPercent + nonAlphaPercent) / 2;

        // Set right string to label
        if (percentResult < 25) {
            labelElem.innerHTML = strings[1];
            labelElem.setAttribute('class', classPrefix + 'Weak');
            indicatorElem.setAttribute('class', 'PasswIndicatorWeak');
            indicatorElem.setAttribute('style', 'height:5px;width:20%;background-color: #ff2631;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'height:5px;width:20%;background-color: #ff2631;';
        }
        else if (percentResult >= 25 && percentResult < 50) {
            labelElem.innerHTML = strings[2];
            labelElem.setAttribute('class', classPrefix + 'Acceptable');
            indicatorElem.setAttribute('class', 'PasswIndicatorAcceptable');
            indicatorElem.setAttribute('style', 'height:5px;width:40%;background-color: #ffd11c;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'height:5px;width:40%;background-color: #ffd11c;';
        }
        else if (percentResult >= 50 && percentResult < 75) {
            labelElem.innerHTML = strings[3];
            labelElem.setAttribute('class', classPrefix + 'Average');
            indicatorElem.setAttribute('class', 'PasswIndicatorAverage');
            indicatorElem.setAttribute('style', 'height:5px;width:60%;background-color: #21bcff;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'height:5px;width:60%;background-color: #21bcff;';
        }
        else if (percentResult >= 75 && percentResult < 100) {
            labelElem.innerHTML = strings[4];
            labelElem.setAttribute('class', classPrefix + 'Strong');
            indicatorElem.setAttribute('class', 'PasswIndicatorStrong');
            indicatorElem.setAttribute('style', 'height:5px;width:80%;background-color: #cdff7c;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'height:5px;width:80%;background-color: #cdff7c;';
            
        }
        else {
            labelElem.innerHTML = strings[5];
            labelElem.setAttribute('class', classPrefix + 'Excellent');
            indicatorElem.setAttribute('class', 'PasswIndicatorExcellent');
            indicatorElem.setAttribute('style', 'height:5px;width:100%;background-color: #00ff00;');
            // Special handling for IE7
            indicatorElem.style.cssText = 'height:5px;width:100%;background-color: #00ff00;';
        }                                 
    }
}


var alphaNum = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';

// Returns whether character is alpha numeric 
function isAlphaNum(param) {
    if (alphaNum.indexOf(param, 0) == -1) { 
        return false;
    }

    return true;
}

