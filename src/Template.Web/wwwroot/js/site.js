var utilities;
(function (utilities) {
    async function postJson(url, body) {
        let res = await fetch(url, {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest',
            },
            credentials: "same-origin",
            body: JSON.stringify(body)
        });
        return res;
    }
    utilities.postJson = postJson;
    async function postJsonT(url, body) {
        const response = await this.postJson(url, body);
        return await response.json();
    }
    utilities.postJsonT = postJsonT;
    async function getJson(url) {
        let res = await fetch(url, {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'X-Requested-With': 'XMLHttpRequest',
            },
            credentials: "same-origin",
        });
        return res;
    }
    utilities.getJson = getJson;
    async function getJsonT(url) {
        const response = await this.getJson(url);
        return await response.json();
    }
    utilities.getJsonT = getJsonT;
    /**
    * Naviga un url evitando di memorizzare la navigazione nell'history, mantenendo la funzionalità di +ctrl per aprire in una nuova tab
    */
    function navigateUrlWithoutHystory(url) {
        if (window.event && window.event && window.event.ctrlKey) {
            //ctrl was held down during the click
            window.open(url);
        }
        else {
            window.location.replace(url);
        }
    }
    utilities.navigateUrlWithoutHystory = navigateUrlWithoutHystory;
    /**
     * Naviga un url mantenendo la funzionalità di +ctrl per aprire in una nuova tab
     */
    function navigateUrlExcludeOnitNotNavigate(url) {
        if (event.target instanceof HTMLButtonElement == false &&
            event.target instanceof HTMLAnchorElement == false &&
            (event.target instanceof HTMLTableCellElement && event.target.hasAttribute('data-onit-not-navigate') == false)) {
            if (window.event && window.event.ctrlKey) {
                window.open(url);
            }
            else {
                window.location.href = url;
            }
        }
    }
    utilities.navigateUrlExcludeOnitNotNavigate = navigateUrlExcludeOnitNotNavigate;
    function composeRelativeUrlWithParams(url, paramName1, paramValue1, paramName2, paramValue2, paramName3, paramValue3, paramName4, paramValue4, paramName5, paramValue5, paramName6, paramValue6, paramName7, paramValue7, paramName8, paramValue8, paramName9, paramValue9, paramName10, paramValue10, paramName11, paramValue11, paramName12, paramValue12, paramName13, paramValue13, paramName14, paramValue14, paramName15, paramValue15, paramName16, paramValue16, paramName17, paramValue17, paramName18, paramValue18, paramName19, paramValue19, paramName20, paramValue20) {
        const urlBase = new URL(url, window.location.origin);
        const params = new URLSearchParams(urlBase.search);
        params.set(paramName1, paramValue1);
        if (paramName2) {
            params.set(paramName2, paramValue2);
        }
        if (paramName3) {
            params.set(paramName3, paramValue3);
        }
        if (paramName4) {
            params.set(paramName4, paramValue4);
        }
        if (paramName5) {
            params.set(paramName5, paramValue5);
        }
        if (paramName6) {
            params.set(paramName6, paramValue6);
        }
        if (paramName7) {
            params.set(paramName7, paramValue7);
        }
        if (paramName8) {
            params.set(paramName8, paramValue8);
        }
        if (paramName9) {
            params.set(paramName9, paramValue9);
        }
        if (paramName10) {
            params.set(paramName10, paramValue10);
        }
        if (paramName11) {
            params.set(paramName11, paramValue11);
        }
        if (paramName12) {
            params.set(paramName12, paramValue12);
        }
        if (paramName13) {
            params.set(paramName13, paramValue13);
        }
        if (paramName14) {
            params.set(paramName14, paramValue14);
        }
        if (paramName15) {
            params.set(paramName15, paramValue15);
        }
        if (paramName16) {
            params.set(paramName16, paramValue16);
        }
        if (paramName17) {
            params.set(paramName17, paramValue17);
        }
        if (paramName18) {
            params.set(paramName18, paramValue18);
        }
        if (paramName19) {
            params.set(paramName19, paramValue19);
        }
        if (paramName20) {
            params.set(paramName20, paramValue20);
        }
        return urlBase.pathname + '?' + params.toString();
    }
    utilities.composeRelativeUrlWithParams = composeRelativeUrlWithParams;
    function alertError(text, duration = 0, onclickCallback = null) {
        alertToastify(text, 'error', duration, onclickCallback);
    }
    utilities.alertError = alertError;
    function alertInfo(text, duration = 0, onclickCallback = null) {
        alertToastify(text, 'info', duration, onclickCallback);
    }
    utilities.alertInfo = alertInfo;
    function alertWarning(text, duration = 0, onclickCallback = null) {
        alertToastify(text, 'warning', duration, onclickCallback);
    }
    utilities.alertWarning = alertWarning;
    function alertSuccess(text, duration = 0, onclickCallback = null) {
        alertToastify(text, 'success', duration, onclickCallback);
    }
    utilities.alertSuccess = alertSuccess;
    function alertToastify(text, level, duration = 0, onClick) {
        Toastify({ close: true, gravity: 'bottom', position: 'left', className: 'onit-toastify onit-toastify-' + level, text: text, duration: duration, callback: onClick }).showToast();
    }
    utilities.alertToastify = alertToastify;
})(utilities || (utilities = {}));
//# sourceMappingURL=site.js.map