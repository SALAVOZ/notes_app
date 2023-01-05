export class ClientBase {
    protected transformOption(options: RequestInit) {
        const token = localStorage.getItem('token');
        options.headers = {
            ...options.headers,
            Authorization: 'Bearer' + token
        };
        return Promise.resolve(options)
    }
}