import ApiClientBase from "../../apiClientBase/ApiClientBase";
import { ToDoItem } from "@/modules/todos/models/ToDoItem";

export class TodoEndpointsDetails {
    private client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getAsync() {
        return await this.client.getAsync<Array<ToDoItem>>("api/todos");
    }

    public async getByIdAsync(id: string) {
        return await this.client.getAsync<ToDoItem>(`api/todos/${id}`);
    }

    public async createAsync(todo: ToDoItem) {
        return await this.client.postAsync<ToDoItem>("api/todos", todo);
    }

    public async updateAsync(todo: ToDoItem) {
        return await this.client.putAsync<ToDoItem>("api/todos", todo);
    }

    public async deleteByIdAsync(id: string) {
        return await this.client.deleteAsync<ToDoItem>(`api/todos/${id}`);
    }
}