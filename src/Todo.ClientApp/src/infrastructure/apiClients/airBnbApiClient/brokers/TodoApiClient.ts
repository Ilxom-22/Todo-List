import ApiClientBase from "../../apiClientBase/ApiClientBase";
import { TodoEndpointsDetails } from "./TodoEndpointsDetails";

export class TodoApiClient {
    private readonly client: ApiClientBase;
    public readonly baseUrl: string;

    constructor() {
        this.baseUrl = "https://todo-list-app.azurewebsites.net";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl
        });

        this.todos = new TodoEndpointsDetails(this.client);
    }

    public todos: TodoEndpointsDetails;
}