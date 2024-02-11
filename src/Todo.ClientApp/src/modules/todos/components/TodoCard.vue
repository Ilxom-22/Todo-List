<template>

    <div class="flex w-full h-20 p-4 px-10 border border-gray-500 gap-x-4 rounded-xl group">

        <!-- Primary Actions -->
        <div class="flex items-center">
            <button class="flex items-center justify-center w-8 h-8 border rounded-full group theme-border"
                    @click="toggleIsDone" :disabled="isOverdue">
                <i v-if="todo.isDone || (!todo.isDone && !isOverdue)" class="simple-hover fa-solid fa-check theme-icon"
                   :class="{ 'text-successColor opacity-100': todo.isDone }"/>
                <i v-if="!todo.isDone && isOverdue"
                   class="fa-solid fa-xmark text-textSecondaryColor"/>
            </button>
        </div>

        <!-- Details -->
        <div class="flex-grow">
            <h5 class="font-bold line-clamp-1">{{ todo.title }}</h5>
            <div class="flex gap-2 text-sm">
                <p class="opacity-80" :class="{ 'text-red-500': !todo.isDone && isOverdue }">
                    <i class="mr-1 fa-regular fa-calendar theme-icon" :class="{ 'text-red-500': !todo.isDone && isOverdue }"/>
                    {{ dateFormatter.formatHumanize(todo.dueTime) }}
                </p>
                <span class="opacity-40">•</span>
                <p class="opacity-40">
                    <i class="mr-1 fa-regular fa-bell theme-icon"></i>
                    {{ dateFormatter.formatHumanize(todo.reminderTime) }}
                </p>
            </div>
        </div>

        <!-- Secondary Actions -->
        <div class="flex items-center transition-opacity duration-200 opacity-0 gap-x-4 group-hover:opacity-100">

            <!-- Favorite button -->
            <button class="text-2xl btn-hover" @click="toggleIsFavorite">
                <i class="fa-star theme-icon" :class="todo.isFavorite ? 'fa-solid' : 'fa-regular'"></i>
            </button>

            <!-- Edit button -->
            <button class="text-2xl btn-hover" @click="onEdit">
                <i class="fa-solid fa-pen-to-square theme-icon"></i>
            </button>

            <!-- Delete button -->
            <button class="text-2xl btn-hover" @click="onDeleteAsync">
                <i class="fa-solid fa-trash theme-icon"/>
            </button>

        </div>

    </div>

</template>

<script setup lang="ts">

import { computed } from "vue";
import { ToDoItem } from "@/modules/todos/models/ToDoItem";
import { TodoApiClient } from "@/infrastructure/apiClients/airBnbApiClient/brokers/TodoApiClient";
import { DateTimeFormatterService } from "@/infrastructure/services/DateCalculatorService";
import { Utils } from "@/infrastructure/extensions/ObjectExtensions";

const todoApiClient = new TodoApiClient();
const dateFormatter = new DateTimeFormatterService();

const props = defineProps({
    todo: {
        type: Object as () => ToDoItem,
        required: true
    }
});

const emit = defineEmits<{
    editTodo: [id: string],
    deleteTodo: [id: string]
}>();

const toggleIsDone = async () => {
    const clonedTodo = Utils.deepClone(props.todo);
    clonedTodo.isDone = !clonedTodo.isDone;

    const response = await todoApiClient.todos.updateAsync(clonedTodo);
    if (response.isSuccess)
        Object.assign(props.todo, clonedTodo);
}

const toggleIsFavorite = async () => {
    const clonedTodo = Utils.deepClone(props.todo);
    clonedTodo.isFavorite = !clonedTodo?.isFavorite;

    const response = await todoApiClient.todos.updateAsync(clonedTodo);
    if (response.isSuccess)
        Object.assign(props.todo, clonedTodo);
}

const onEdit = () => {
    emit('editTodo', props.todo?.id);
}

const onDeleteAsync = async () => {
    const response = await todoApiClient.todos.deleteByIdAsync(props.todo.id);
    if (response.isSuccess)
        emit("deleteTodo", props.todo.id);
}

// create computed properties
const isOverdue = computed(() => new Date(props.todo.dueTime) < new Date());

</script>@/infrastructure/apiClients/airBnbApiClient/todoListApiClients/brokers/TodoApiClient