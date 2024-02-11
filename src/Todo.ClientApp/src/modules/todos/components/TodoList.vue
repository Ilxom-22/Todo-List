<template>

    <div class="flex flex-col gap-4 w-full h-[800px] overflow-x-scroll no-scrollbar">
        <todo-card v-for="todo in todos" :key="todo.id" :todo="todo" @editTodo="onEditTodo" @deleteTodo="onDeleteTodo"/>
    </div>

</template>

<script setup lang="ts">

import { defineProps, defineEmits } from "vue";
import TodoCard from "@/modules/todos/components/TodoCard.vue";
import type { ToDoItem } from "@/modules/todos/models/ToDoItem";
import type { PropType } from "vue";

const props = defineProps({
    todos: {
        type: Array as PropType<Array<ToDoItem>>,
        required: true
    }
});

const emit = defineEmits<{
    editTodo: [id: string],
}>();

const onEditTodo = (id: string) => {
    emit("editTodo", id);
}

const onDeleteTodo = (id: string) => {
    const index = props.todos.findIndex(x => x.id === id);
    if (index !== -1)
        props.todos.splice(index, 1);
}

</script>