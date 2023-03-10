import {ClientBase} from "./client-base";
export class Client extends ClientBase {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super();
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    /**
     * Get the list of notes
     * @return Success
     */
    getAll(version: string): Promise<NoteListVm> {
        let url_ = this.baseUrl + "/api/{version}/Note";
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetAll(_response);
        });
    }

    protected processGetAll(response: Response): Promise<NoteListVm> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
                let result200: any = null;
                result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as NoteListVm;
                return result200;
            });
        } else if (status === 401) {
            return response.text().then((_responseText) => {
                let result401: any = null;
                result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
                return throwException("If the user is unauthorized", status, _responseText, _headers, result401);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<NoteListVm>(null as any);
    }

    /**
     * Creates a new note
     * @param body (optional) CreateNoteDto object
     * @return Success
     */
    create(version: string, body: CreateNoteDto | undefined): Promise<string> {
        let url_ = this.baseUrl + "/api/{version}/Note";
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processCreate(_response);
        });
    }

    protected processCreate(response: Response): Promise<string> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
                let result200: any = null;
                result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as string;
                return result200;
            });
        } else if (status === 401) {
            return response.text().then((_responseText) => {
                let result401: any = null;
                result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
                return throwException("If the user is unauthorized", status, _responseText, _headers, result401);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<string>(null as any);
    }

    /**
     * Updates the note
     <remarks>
     Sample request:
     PUT /note
     {
        title: "Updated note title"
    }
     </remarks>
     * @param body (optional) UpdateNoteDto object
     * @return Success
     */
    update(version: string, body: UpdateNoteDto | undefined): Promise<void> {
        let url_ = this.baseUrl + "/api/{version}/Note";
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_: RequestInit = {
            body: content_,
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processUpdate(_response);
        });
    }

    protected processUpdate(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
                return;
            });
        } else if (status === 401) {
            return response.text().then((_responseText) => {
                let result401: any = null;
                result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
                return throwException("If the user is unauthorized", status, _responseText, _headers, result401);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }

    /**
     * Get the note by id
     * @param id Note id
     * @return Success
     */
    get(id: string, version: string): Promise<NoteDetailsVm> {
        let url_ = this.baseUrl + "/api/{version}/Note/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: Response): Promise<NoteDetailsVm> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
                let result200: any = null;
                result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as NoteDetailsVm;
                return result200;
            });
        } else if (status === 401) {
            return response.text().then((_responseText) => {
                let result401: any = null;
                result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
                return throwException("Is the user is unauthorized", status, _responseText, _headers, result401);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<NoteDetailsVm>(null as any);
    }

    /**
     * Delete the note by note's id
     * @param id Id of the note
     * @return Success
     */
    delete(id: string, version: string): Promise<void> {
        let url_ = this.baseUrl + "/api/{version}/Note/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        if (version === undefined || version === null)
            throw new Error("The parameter 'version' must be defined.");
        url_ = url_.replace("{version}", encodeURIComponent("" + version));
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "DELETE",
            headers: {
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processDelete(_response);
        });
    }

    protected processDelete(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
                return;
            });
        } else if (status === 401) {
            return response.text().then((_responseText) => {
                let result401: any = null;
                result401 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ProblemDetails;
                return throwException("If the user is authorized", status, _responseText, _headers, result401);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
                return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }
}

export interface CreateNoteDto {
    title: string;
    details?: string | undefined;
}

export interface NoteDetailsVm {
    id?: string;
    title?: string | undefined;
    details?: string | undefined;
    creationDate?: Date;
    editTime?: Date | undefined;
}

export interface NoteListVm {
    notes?: NoteLookupDto[] | undefined;
}

export interface NoteLookupDto {
    id?: string;
    title?: string | undefined;
}

export interface ProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;

    [key: string]: any;
}

export interface UpdateNoteDto {
    id?: string;
    title?: string | undefined;
    details?: string | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}