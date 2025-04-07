<template>
  <div>
    <h1>Todo List</h1>

    <ul>
      <li v-for="todo in todos" :key="todo.id">{{ todo.title }} – {{ todo.isDone ? '✅' : '❌' }}</li>
    </ul>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { type TodoItem, Client } from './api/client'

const todos = ref<TodoItem[]>([])

const api = new Client('http://localhost:5101')

onMounted(async () => {
  todos.value = await api.getTodos()
})
</script>
