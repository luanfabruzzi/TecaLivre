import { NavLink, Route, Routes } from 'react-router-dom'

const paginas = ['Dashboard', 'Empréstimos', 'Acervo', 'Alunos', 'Relatórios', 'Configurações']
const rota = (nome: string) => nome.toLowerCase().normalize('NFD').replace(/[\u0300-\u036f]/g, '')

function Dashboard() {
  const indicadores = ['Empréstimos ativos', 'Devoluções hoje', 'Em atraso', 'Exemplares disponíveis']
  return <><header><p className="eyebrow">VISÃO GERAL</p><h1>Olá! Vamos cuidar da biblioteca.</h1><p>A nova base está pronta para receber as telas de operação.</p></header><section className="cards">{indicadores.map((titulo, i) => <article key={titulo}><span>{titulo}</span><strong>{i === 2 ? '0' : '—'}</strong></article>)}</section><section className="panel"><h2>Próximas devoluções</h2><p>Nenhum empréstimo cadastrado ainda.</p></section></>
}

function EmConstrucao({ nome }: { nome: string }) { return <header><p className="eyebrow">MÓDULO</p><h1>{nome}</h1><p>Estrutura preparada. Esta tela será implementada na próxima etapa.</p></header> }

export default function App() {
  return <div className="shell"><aside><div className="brand">T<span>•</span><div>TecaLivre<small>A sua biblioteca open source</small></div></div><nav>{paginas.map((p, i) => <NavLink key={p} to={i ? `/${rota(p)}` : '/'} end={i === 0}>{p}</NavLink>)}</nav><footer>TecaLivre · Livre e local</footer></aside><main><Routes><Route path="/" element={<Dashboard />}/>{paginas.slice(1).map(p => <Route key={p} path={`/${rota(p)}`} element={<EmConstrucao nome={p}/>}/>)}</Routes></main></div>
}
