<template>
  <div>
    <h1>Todo List</h1>

    <ul>
      <li v-for="todo in todos" :key="todo.id">
        {{ todo.title }} – {{ todo.isDone ? '✅' : '❌' }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts" setup>
  import { ref, onMounted } from 'vue'
  import type { TodoItem } from './api/models'
  import { DefaultApi } from './api/apis/DefaultApi'
  import { Configuration } from './api/runtime'

  const todos = ref<TodoItem[]>([])

  onMounted(async () => {
    const api = new DefaultApi(new Configuration({ basePath: 'http://localhost:5101' }))
    todos.value = await api.getTodos()
  })
</script>
