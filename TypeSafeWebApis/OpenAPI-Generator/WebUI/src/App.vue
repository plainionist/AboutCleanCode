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
  import type { TodoItem } from './api/models'

  const todos = ref<TodoItem[]>([])

  onMounted(async () => {
    const response = await fetch('http://localhost:5101/todos')
    todos.value = await response.json()
  })
</script>
