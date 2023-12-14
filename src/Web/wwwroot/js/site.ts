declare var Toastify: any;

namespace utilities {
    export async function postJson(url: string, body: any): Promise<Response> {
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

    export async function postJsonT<T>(url: string, body: any): Promise<T> {
        const response = await this.postJson(url, body);
        return await response.json();
    }

    export async function getJson(url: string): Promise<Response> {
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

    export async function getJsonT<T>(url: string): Promise<T> {
        const response = await this.getJson(url);
        return await response.json();
    }

    /**
    * Naviga un url evitando di memorizzare la navigazione nell'history, mantenendo la funzionalità di +ctrl per aprire in una nuova tab
    */
    export function navigateUrlWithoutHystory(url: string) {
        if (window.event && (<KeyboardEvent>window.event) && (<KeyboardEvent>window.event).ctrlKey) {
            //ctrl was held down during the click
            window.open(url)
        } else {
            window.location.replace(url);
        }
    }

    /**
     * Naviga un url mantenendo la funzionalità di +ctrl per aprire in una nuova tab
     */
    export function navigateUrlExcludeOnitNotNavigate(url: string) {
        if (event.target instanceof HTMLButtonElement == false &&
            event.target instanceof HTMLAnchorElement == false &&
            (event.target instanceof HTMLTableCellElement && event.target.hasAttribute('data-onit-not-navigate') == false)
        ) {
            if (window.event && (<KeyboardEvent>window.event).ctrlKey) {
                window.open(url)
            } else {
                window.location.href = url;
            }
        }
    }

    export function composeRelativeUrlWithParams(url: string,
        paramName1: string, paramValue1: string,
        paramName2?: string, paramValue2?,
        paramName3?: string, paramValue3?,
        paramName4?: string, paramValue4?,
        paramName5?: string, paramValue5?,
        paramName6?: string, paramValue6?,
        paramName7?: string, paramValue7?,
        paramName8?: string, paramValue8?,
        paramName9?: string, paramValue9?,
        paramName10?: string, paramValue10?,
        paramName11?: string, paramValue11?,
        paramName12?: string, paramValue12?,
        paramName13?: string, paramValue13?,
        paramName14?: string, paramValue14?,
        paramName15?: string, paramValue15?,
        paramName16?: string, paramValue16?,
        paramName17?: string, paramValue17?,
        paramName18?: string, paramValue18?,
        paramName19?: string, paramValue19?,
        paramName20?: string, paramValue20?
    ): string {
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

    export function alertError(text: string, duration = 0, onclickCallback = null) {
        alertToastify(text, 'error', duration, onclickCallback);
    }

    export function alertInfo(text: string, duration = 0, onclickCallback = null) {
        alertToastify(text, 'info', duration, onclickCallback);
    }

    export function alertWarning(text: string, duration = 0, onclickCallback = null) {
        alertToastify(text, 'warning', duration, onclickCallback);
    }

    export function alertSuccess(text: string, duration = 0, onclickCallback = null) {
        alertToastify(text, 'success', duration, onclickCallback);
    }

    export function alertToastify(text: string, level: string, duration = 0, onClick: Function) {
        Toastify({ close: true, gravity: 'bottom', position: 'left', className: 'onit-toastify onit-toastify-' + level, text: text, duration: duration, callback: onClick }).showToast();
    }
}